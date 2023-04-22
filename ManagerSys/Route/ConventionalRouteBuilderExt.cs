using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc.Conventions;
using Volo.Abp.Reflection;

namespace ManagerSys.Host.Route
{
    /// <summary>
    /// Abp原生路由，去除rootPath:app
    /// </summary>
    public class ConventionalRouteBuilderExt : ConventionalRouteBuilder
    {
        public ConventionalRouteBuilderExt(IOptions<AbpConventionalControllerOptions> options) : base(options)
        {
        }

        public override string Build(string rootPath, string controllerName, ActionModel action, string httpMethod, ConventionalControllerSetting configuration)
        {
            var controllerNameInUrl = NormalizeUrlControllerName(rootPath, controllerName, action, httpMethod, configuration);

            var url = $"api/{NormalizeControllerNameCase(controllerNameInUrl, configuration)}";

            //Add {id} path if needed
            var idParameterModel = action.Parameters.FirstOrDefault(p => p.ParameterName == "id");
            if (idParameterModel != null)
            {
                if (TypeHelper.IsPrimitiveExtended(idParameterModel.ParameterType, includeEnums: true))
                {
                    url += "/{id}";
                }
                else
                {
                    var properties = idParameterModel
                        .ParameterType
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public);

                    foreach (var property in properties)
                    {
                        url += "/{" + NormalizeIdPropertyNameCase(property, configuration) + "}";
                    }
                }
            }

            //Add action name if needed
            var actionNameInUrl = NormalizeUrlActionName(rootPath, controllerName, action, httpMethod, configuration);
            if (!AbpStringExtensions.IsNullOrEmpty(actionNameInUrl))
            {
                url += $"/{NormalizeActionNameCase(actionNameInUrl, configuration)}";

                //Add secondary Id
                var secondaryIds = action.Parameters
                    .Where(p => p.ParameterName.EndsWith("Id", StringComparison.Ordinal)).ToList();
                if (secondaryIds.Count == 1)
                {
                    url += $"/{{{NormalizeSecondaryIdNameCase(secondaryIds[0], configuration)}}}";
                }
            }

            return url;
        }
    }

}
