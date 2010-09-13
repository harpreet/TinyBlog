using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace TinyBlog
{
    public class CustomControllerFactory : IControllerFactory
    {
        private readonly Dictionary<string, Type> _controllerNamesToControllers = new Dictionary<string, Type>();

        public CustomControllerFactory()
        {
            PopulateControllerMap();
        }

        private void PopulateControllerMap()
        {
            IEnumerable<Type> controllers = FindDerivedTypes(Assembly.GetExecutingAssembly(), typeof(Controller), true);
            AddControllers(controllers);
        }

        public void AddControllers(IEnumerable<Type> controllers)
        {
            foreach(var c in controllers)
            {
                AddController(c);
            }
        }

        public void AddController(Type controller)
        {
            _controllerNamesToControllers.Add(controller.Name.Replace("Controller", string.Empty), controller);

        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (controllerName == null) throw new ArgumentNullException("controllerName");
            
            Type controllerType = _controllerNamesToControllers[controllerName];

            if (controllerType == null) throw new ArgumentException("Invalid controller type");

            ObjectFactory.Inject(requestContext.HttpContext);

            var controller = ObjectFactory.GetInstance(controllerType) as IController;

            if (controller == null) throw new ArgumentException("Can't create controller");

            return controller;
        }

        public void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;

            if (disposableController != null) disposableController.Dispose();
        }


        /// <summary>
        /// http://www.codemeit.com/code-collection/c-find-all-derived-types-from-assembly.html
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="baseType"></param>
        /// <param name="canBeInstantiated"></param>
        /// <returns></returns>
        public static List<Type> FindDerivedTypes(Assembly assembly, Type baseType, bool canBeInstantiated)
        {
            var results = new List<Type>();
            if (assembly == null)
                throw new ArgumentNullException("assembly", "Assembly must be defined");

            if (baseType == null)
                throw new ArgumentNullException("baseType", "Parent Type must be defined");

            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (canBeInstantiated && (!type.IsClass || type.IsAbstract)) continue;

                if (baseType.IsInterface)
                {
                    var it = type.GetInterface(baseType.FullName);

                    if (it != null) results.Add(type);
                }
                else if (type.IsSubclassOf(baseType))
                {
                    results.Add(type);
                }
            }

            return results;
        }
    }
}
