using Microsoft.CSharp;
using SqlSugar;
using System.CodeDom;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace YuG.Framework.Generators
{
    /// <summary>
    /// DbFirst帮助类
    /// </summary>
    public class DbFirstHelper
    {
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="option"></param>
        public static void BuildEntity(BuildEntityOption option)
        {
            var db = BuildCodeHelper.GetDbClient(option.DbOption);

            //是否生成所有表
            bool isAllTable = option.EntityInfos is null || option.EntityInfos.Count() == 0;

            var tables = db.DbMaintenance.GetTableInfoList();
            string baseClass = option.IsExtend ? ": BaseEntity" : "";

            //获取所有需要生成实体的表，并且设置表名映射
            foreach (var item in tables)
            {
                if (isAllTable)
                {
                    string entityName = BuildCodeHelper.UpperEntity(item.Name);
                    db.MappingTables.Add(entityName, item.Name);
                }
                else
                {
                    var entityInfo = option.EntityInfos!.FirstOrDefault(e => e.TableName == item.Name);
                    if (entityInfo is not null)
                    {
                        string entityName = entityInfo.EntityName ?? BuildCodeHelper.UpperEntity(item.Name);
                        db.MappingTables.Add(entityName, item.Name);
                    }
                }
            }
            Console.WriteLine($"【Generators】当前需要生成实体的表：{string.Join(',', db.MappingTables.Select(e => e.DbTableName))}");
            Console.WriteLine($"【Generators】实体生成路径：{option.Path}");
            foreach (var item in db.MappingTables)
            {
                //实体名称
                var entityName = db.MappingTables.Where(e => e.DbTableName == item.DbTableName)
                                                 .Select(e => e.EntityName)
                                                 .FirstOrDefault();
                //表信息
                var tableInfo = tables.FirstOrDefault(e => e.Name == item.DbTableName)!;
                //表字段信息
                var columns = db.DbMaintenance.GetColumnInfosByTableName(item.DbTableName);
                var ignoreDbColumn = BuildCodeHelper.IgnoreDbColumn();
                foreach (var column in columns)
                {
                    string propertyName = BuildCodeHelper.UpperStr(column.DbColumnName);
                    db.MappingColumns.Add(propertyName, column.DbColumnName, entityName);
                    if (option.IsExtend && ignoreDbColumn.Contains(column.DbColumnName))
                    {
                        db.IgnoreColumns.Add(column.DbColumnName, entityName);
                    }

                }

                string className = item.EntityName ?? BuildCodeHelper.UpperEntity(item.DbTableName!);
                db.DbFirst.Where(item.DbTableName)
                          .IsCreateAttribute()
                          .SettingClassTemplate(e =>
                          {
                              //设置模板
                              string classTemplate = @"{using}

namespace {Namespace}
{
    {ClassDescription}
    {SugarTable}
    public partial class {ClassName} " + baseClass + @"
    {
        public " + className + @"()
        {
            {Constructor}
        }

{PropertyName}
    }
}
";
                              classTemplate = classTemplate.Replace("{SugarTable}", $@"[SqlSugar.SugarTable(""{tableInfo.Name}"")]");
                              classTemplate = classTemplate.Replace("{ClassName}", className);
                              return classTemplate;
                          })
                          .SettingNamespaceTemplate(e =>
                          {
                              //Using Area
                              e += "using YuG.Framework;\n";
                              return e;
                          })
                          .SettingClassDescriptionTemplate(e =>
                          {
                              //Class Description Area
                              string classDesc = @"
    /// <summary>
    /// TableName: " + tableInfo.Name + @"
    /// TableDescription: " + tableInfo.Description + @"
    /// </summary>";
                              return classDesc;
                          })
                          .SettingConstructorTemplate(e =>
                          {
                              //Constructor Area
                              return e;
                          })
                          .SettingPropertyDescriptionTemplate(e =>
                          {
                              string propertyDesc = @"
        /// <summary>
        /// Desc: {PropertyDescription}
        /// Default: {DefaultValue}
        /// Nullable: {IsNullable}
        /// </summary>";
                              return propertyDesc;
                          })
                          .SettingPropertyTemplate(e =>
                          {
                              string propertyTemp = @"{SugarColumn}
        public {PropertyType} {PropertyName} { get; set; }
";
                              return propertyTemp;
                          })
                          .CreateClassFile($"{option.Path}", option.NameSpace);

                Console.WriteLine($"【Generators】实体：{className}，表：{item.DbTableName}。文件创建成功");
            }
        }

        /// <summary>
        /// 创建业务代码
        /// </summary>
        public static void BuildServiceCode(BuildServiceCodeOption option)
        {
            option.Check();
            string path = option.BasePath + "\\" + option.ServiceNameSpace;
            //判断Service层是否存在
            if (Directory.Exists(path))
            {
                //Service层下的Domain根目录
                string domainPath = path + "\\Domain\\" + option.DomainName;
                if (!Directory.Exists(domainPath))
                    Directory.CreateDirectory(domainPath);

                //Domain NameSpace
                string nameSpace = $"{option.ServiceNameSpace}.Domain.{option.DomainName}";

                //Build Model
                var pairs = BuildModelClassCode(option, option.DTOName, domainPath, nameSpace);

                BuildMapperClassCode(option, domainPath, nameSpace, pairs);

                //Build Interface Repository
                string repositoryName = BuildRepositoryClassCode(option, domainPath, nameSpace);

                //Build Repository
                BuildRepositoryLayerCode(option, repositoryName, nameSpace);

                //Build Service
                string serviceName = BuildServiceClassCode(option, domainPath, nameSpace, repositoryName, pairs);

                //Build Controller
                BuildControllerLayerCode(option, nameSpace, serviceName, pairs);
            }
            else
            {
                throw new Exception($"【Generators】业务层不存在，请检查地址是否正确。{path}");
            }
        }

        /// <summary>
        /// 创建仓储层实现类
        /// </summary>
        /// <param name="option"></param>
        /// <param name="irepositoryName"></param>
        /// <param name="irepositoryNameSpace"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string BuildRepositoryLayerCode(BuildServiceCodeOption option,
                                                       string irepositoryName,
                                                       string irepositoryNameSpace)
        {
            string path = option.BasePath + "\\" + option.RepositoryNameSpace;
            if (!Directory.Exists(path))
                throw new Exception($"【Generators】仓储层不存在，请检查地址是否正确。{path}");

            string domainPath = path + "\\Domain\\" + option.DomainName;
            if (!Directory.Exists(domainPath))
                Directory.CreateDirectory(domainPath);

            //Class NameSpace
            string nameSpace = $"{option.RepositoryNameSpace}.Domain.{option.DomainName}";

            //Class Name
            string repositoryName = option.RepositoryName ?? option.DomainName;
            repositoryName = $"{repositoryName}Repository";

            //Class File Path
            string filePath = domainPath + "\\" + repositoryName + ".cs";

            //Class Content
            string content = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YuG.Framework.ORM;
using " + irepositoryNameSpace + @";

namespace " + nameSpace + @"
{
    public class " + repositoryName + @" : BaseRepository, " + irepositoryName + @"
    {
        public " + repositoryName + @"(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
";

            File.WriteAllText(filePath, content);

            return repositoryName;
        }

        /// <summary>
        /// 创建Controller层文件
        /// </summary>
        /// <param name="option"></param>
        /// <param name="serviceNameSpace"></param>
        /// <param name="serviceName"></param>
        /// <param name="pairs"></param>
        /// <exception cref="Exception"></exception>
        private static void BuildControllerLayerCode(BuildServiceCodeOption option,
                                                     string serviceNameSpace,
                                                     string serviceName,
                                                     Dictionary<ModelTypeEnum, string> pairs)
        {
            string path = option.BasePath + "\\" + option.ControllerNameSpace + "\\Controllers";
            if (Directory.Exists(path))
            {
                string controllerPath = path + "\\" + option.DomainName + "\\";
                if (!Directory.Exists(controllerPath))
                    Directory.CreateDirectory(controllerPath);

                string fileName = $"{option.ServiceName ?? option.DomainName}Controller";
                string nameSpace = $"{option.ControllerNameSpace}.{option.DomainName}";
                string content = @"using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using " + serviceNameSpace + @";

namespace " + nameSpace + @"
{
    /// <summary>
    /// " + option.ServiceDetails + @"
    /// </summary>
    [Route(""api/[controller]"")]
    [ApiController]
    public class " + fileName + @" : ControllerBase
    {
        private readonly " + serviceName + @" _service;

        /// <summary>
        /// init
        /// </summary>
        public " + fileName + @"(" + serviceName + @" service)
        {
            this._service = service;
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name=""query""></param>
        /// <returns></returns>
        [HttpGet(""list"")]
        public async Task<IActionResult> List([FromQuery] " + pairs[ModelTypeEnum.Query] + @" query)
        {
            return Ok(BuildResultObject.BuildSuccessResult(await _service.GetListAsync(query)));
        }

        /// <summary>
        /// 根据id查询数据
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        [HttpGet(""get"")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(BuildResultObject.BuildSuccessResult(await _service.GetDataAsync(id)));
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name=""dto""></param>
        /// <returns></returns>
        [HttpPost(""add"")]
        public async Task<IActionResult> Add([FromBody] " + pairs[ModelTypeEnum.AddDTO] + @" dto)
        {
            return Ok(await _service.AddAsync(dto));
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name=""dto""></param>
        /// <returns></returns>
        [HttpPut(""edit"")]
        public async Task<IActionResult> Edit([FromBody] " + pairs[ModelTypeEnum.EditDTO] + @" dto)
        {
            return Ok(await _service.EditAsync(dto));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        [HttpDelete(""delete"")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}";
                File.WriteAllText($"{controllerPath}{fileName}.cs", content);
            }
            else
            {
                throw new Exception($"【Generators】Controller层不存在，请检查地址是否正确。{path}");
            }
        }


        /// <summary>
        /// 检查Service层文件目录是否存在
        /// </summary>
        /// <param name="option"></param>
        /// <param name="slnPath"></param>
        /// <returns></returns>
        private static string CheckDirectory(BuildServiceCodeOption option, string slnPath)
        {
            string domainPath = slnPath + "\\Domain\\" + option.DomainName;
            if (!Directory.Exists(domainPath))
                Directory.CreateDirectory(domainPath);

            return domainPath;
        }

        /// <summary>
        /// 创建Service代码
        /// </summary>
        /// <param name="option"></param>
        /// <param name="nameSpace"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static string BuildServiceClassCode(BuildServiceCodeOption option,
                                                  string domainPath,
                                                  string nameSpace,
                                                  string repositoryName,
                                                  Dictionary<ModelTypeEnum, string> pairs)
        {
            string servicePath = domainPath + "\\Service";
            if (!Directory.Exists(servicePath))
                Directory.CreateDirectory(servicePath);

            //Class Name
            string serviceName = string.IsNullOrWhiteSpace(option.ServiceName) ? option.DomainName : option.ServiceName;
            serviceName += "Service";
            //Class File Path
            string filePath = servicePath + "\\" + serviceName + ".cs";
            //Class Content
            string content = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YuG.Framework.ORM;
using Microsoft.Extensions.Logging;
using YuG.Framework.Authorization;
using YuG.Framework.Core;
using " + option.EntityNameSpace + @";

namespace " + nameSpace + @"
{
    /// <summary>
    /// " + (option.ServiceDetails ?? serviceName) + @"
    /// </summary>
    public class " + serviceName + @"
    {
        private readonly " + repositoryName + @" _repository;
        private readonly ILogger<" + serviceName + @"> _logger;
        private readonly IMapper _mapper;
        private readonly IUserInfoHelper _userInfo;

        public " + serviceName + @"(" + repositoryName + @" repository,
                                   ILogger<" + serviceName + @"> logger,
                                   IMapper mapper,
                                   IUserInfoHelper userInfo)
        {
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
            this._userInfo = userInfo;
        }

        /// <summary>
        /// Get Data By ID
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        public async Task<" + option.EntityName + @"> GetDataAsync(int id)
        {
            return await _repository.GetFindByIDAsync<" + option.EntityName + @">(id);
        }

        /// <summary>
        /// Get Data List 
        /// </summary>
        /// <param name=""query""></param>
        /// <returns></returns>
        public async Task<PageModel<" + pairs[ModelTypeEnum.VO] + @">> GetListAsync(" + pairs[ModelTypeEnum.Query] + @" query)
        {
            var data = await _repository.GetPageAsync<" + option.EntityName + @", 
                                                      " + pairs[ModelTypeEnum.VO] + @">(query,
                                                                    e => e.DelFlag == Core.DbEnum.DelFlag.UnDelete);
            return data;
        }

        /// <summary>
        /// Add Data 
        /// </summary>
        /// <param name=""dto""></param>
        /// <returns></returns>
        public async Task<ResultObject<int>> AddAsync(" + pairs[ModelTypeEnum.AddDTO] + @" dto)
        {
            var model = new " + option.EntityName + @"();
            _mapper.Map(dto, model);

            model.CreateUser = _userInfo.GetCurrentUserID().ToString();
            model.CreateTime = DateTime.Now;
            model.DelFlag = DbEnum.DelFlag.UnDelete;
            
            int res = await _repository.AddAsync(model);
            return res > 0 ? BuildResultObject.BuildSuccessResult(res) : BuildResultObject.BuildCustomizeResultData(ResultEnum.DataSaveFail, 0);
        }

        /// <summary>
        /// Edit Data
        /// </summary>
        /// <param name=""dto""></param>
        /// <returns></returns>
        public async Task<ResultObject<object>> EditAsync(" + pairs[ModelTypeEnum.EditDTO] + @" dto)
        {
            var model = await _repository.GetFindByIDAsync<" + option.EntityName + @">(dto.ID);
            if (model is null)
                return BuildResultObject.BuildCustomizeResult(ResultEnum.DataNotFound, $""ID: 【{ dto.ID }】 不存在"");

            _mapper.Map(dto, model);

            model.UpdateUser = _userInfo.GetCurrentUserID().ToString();
            model.UpdateTime = DateTime.Now;
            
            return await _repository.EditAsync(model) ? 
                         BuildResultObject.BuildSuccessResult() : 
                         BuildResultObject.BuildCustomizeResult(ResultEnum.DataSaveFail);
        }

        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        public async Task<ResultObject<object>> DeleteAsync(int id)
        {
            var model = await _repository.GetFindByIDAsync<" + option.EntityName + @">(id);
            if (model is null)
                return BuildResultObject.BuildCustomizeResult(ResultEnum.DataNotFound, $""ID: 【{ id }】 不存在"");

            model.UpdateUser = _userInfo.GetCurrentUserID().ToString();
            model.UpdateTime = DateTime.Now;

            return await _repository.EditDelFlagAsync(model) ? 
                         BuildResultObject.BuildSuccessResult() : 
                         BuildResultObject.BuildCustomizeResult(ResultEnum.DataSaveFail);
        }
    }
}
";

            File.WriteAllText(filePath, content);

            return serviceName;
        }

        /// <summary>
        /// 创建DTO对象
        /// </summary>
        /// <param name="pairs"></param>
        /// <param name="modelPath"></param>
        private static Dictionary<ModelTypeEnum, string> BuildModelClassCode(BuildServiceCodeOption option, string dtoName, string domainPath, string nameSpace)
        {
            string modelPath = domainPath + "\\Model";
            if (!Directory.Exists(modelPath))
                Directory.CreateDirectory(modelPath);

            Dictionary<ModelTypeEnum, string> pairs = new Dictionary<ModelTypeEnum, string>();
            pairs.Add(ModelTypeEnum.AddDTO, $"Add{dtoName}DTO");
            pairs.Add(ModelTypeEnum.EditDTO, $"Edit{dtoName}DTO");
            pairs.Add(ModelTypeEnum.Query, $"{dtoName}Query");
            pairs.Add(ModelTypeEnum.VO, $"{dtoName}VO");

            //Get Entity Property
            var type = Assembly.LoadFrom(option.EntityDllPath)
                                      .GetTypes()
                                      .Where(e => e.Name == option.EntityName).FirstOrDefault();
            if (type is null)
                throw new Exception($"【Generators】实体名称：{option.EntityName}，DLL路径：{option.EntityDllPath}。未找到类型");

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            StringBuilder property = new StringBuilder();
            foreach (var propertyInfo in properties)
            {
                using var provider = new CSharpCodeProvider();
                var typeRef = new CodeTypeReference(propertyInfo.PropertyType);
                string typeName = provider.GetTypeOutput(typeRef);
                if (typeName.Contains("System.Nullable"))
                {
                    Regex re = new Regex(@"(?<=\<)[a-zA-Z_]+(?=\>)");
                    string value = re.Match(typeName).Value;
                    typeName = value + "?";
                }

                property.AppendLine("        public " + typeName + " " + propertyInfo.Name + " { get; set; }");
            }

            foreach (var item in pairs)
            {
                string baseClass = string.Empty,
                       propertyStr = string.Empty;
                if (item.Key == ModelTypeEnum.Query)
                    baseClass = " : BaseQuery";
                else if (item.Key == ModelTypeEnum.EditDTO || item.Key == ModelTypeEnum.VO)
                    propertyStr = "        public int ID { get; set; }\n" + property.ToString();
                else
                    propertyStr = property.ToString();


                string content = @"using System;
using YuG.Framework;

namespace " + nameSpace + @"
{
    public class " + (item.Value + baseClass) + @"
    {
" + propertyStr + @"
    }
}";
                File.WriteAllText(modelPath + "\\" + item.Value + ".cs", content);
            }

            return pairs;
        }

        /// <summary>
        /// 创建仓储层接口
        /// </summary>
        /// <param name="option"></param>
        /// <param name="repositoryPath"></param>
        /// <param name="nameSpace"></param>
        private static string BuildRepositoryClassCode(BuildServiceCodeOption option, string domainPath, string nameSpace)
        {
            string repositoryPath = domainPath + "\\IRepository";
            if (!Directory.Exists(repositoryPath))
                Directory.CreateDirectory(repositoryPath);

            //Class Name
            string repositoryName = option.RepositoryName ?? option.DomainName;
            repositoryName = $"I{repositoryName}Repository";

            string content = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuG.Framework;
using YuG.Framework.ORM;

namespace " + nameSpace + @"
{
    public interface " + repositoryName + @" : IBaseRepository
    {

    }
}
";
            File.WriteAllText(repositoryPath + "\\" + repositoryName + ".cs", content);

            return repositoryName;
        }

        /// <summary>
        /// 创建Mapper文件
        /// </summary>
        /// <param name="option"></param>
        /// <param name="domainPath"></param>
        /// <param name="nameSpace"></param>
        /// <param name="pairs"></param>
        private static void BuildMapperClassCode(BuildServiceCodeOption option,
                                                 string domainPath,
                                                 string nameSpace,
                                                 Dictionary<ModelTypeEnum, string> pairs)
        {
            string repositoryPath = domainPath + "\\Mapper";
            if (!Directory.Exists(repositoryPath))
                Directory.CreateDirectory(repositoryPath);

            string mapperName = $"{option.ServiceName ?? option.DomainName}Profile";
            string content = @"using AutoMapper;
using " + option.EntityNameSpace + @";

namespace " + nameSpace + @"
{
    public class " + mapperName + @" : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public " + mapperName + @"()
        {
            CreateMap<" + pairs[ModelTypeEnum.AddDTO] + @", " + option.EntityName + @">();
            CreateMap<" + pairs[ModelTypeEnum.EditDTO] + @", " + option.EntityName + @">();
            CreateMap<" + option.EntityName + @", " + pairs[ModelTypeEnum.VO] + @">();
        }
    }
}";

            File.WriteAllText(repositoryPath + "\\" + mapperName + ".cs", content);
        }


    }

    public enum ModelTypeEnum
    {
        AddDTO,
        EditDTO,
        VO,
        Query
    }
}
