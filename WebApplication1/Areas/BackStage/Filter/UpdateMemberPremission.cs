﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.Models;

namespace WebApplication1.Areas.BackStage.Filter
{
    public class UpdateMemberPremission: ActionFilterAttribute  // ActionFilterAttribute   IActionFilter
    {
        //private DbModel db = new DbModel(); //不能用全域

        //private readonly IDependencyResolver _service;

        //public UpdateMemberPremission(IDependencyResolver service)
        //{
        //    _service = service;
        //}



        //public void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    throw new NotImplementedException();
        //}

        public override  void OnActionExecuting(ActionExecutingContext filterContext)  //override
        {
            DbModel db = new DbModel();

             string TreeViewJSdataArray = ShowTreeStructure(); // 建立 TreeView結構 
            filterContext.Controller.TempData["TreeViewData"] = TreeViewJSdataArray;
            filterContext.Controller.ViewBag.TreeViewData = TreeViewJSdataArray;

            if (filterContext.RouteData.Values["Id"] != null)        // 回饋 當前帳戶的Premission資料結構 
            {
                int userId = Convert.ToInt32(filterContext.RouteData.Values["Id"].ToString());
                var userPermission = db.Member.FirstOrDefault(x => x.Id == userId).Permission;
                if (userPermission != null)
                {
                    string currentPermissionTreeList = ShowCurrentPermissionTree(userPermission);
                    filterContext.Controller.ViewBag.TreeViewCurrentPermission = currentPermissionTreeList;
                }
                else
                {
                    filterContext.Controller.ViewBag.TreeViewCurrentPermission = "[]";
                }
            }

        }
        public string ShowTreeStructure() 
        {
            DbModel db = new DbModel();

            List<Permission> PremmsionList = db.Permission.ToList();
            var roots = PremmsionList.Where(x => x.ParentTable == null);
            StringBuilder data = new StringBuilder("[");
            foreach (var child in roots)  //child is pression
            {
                TreeViewRecurive(child, data);
                data.Append(",");
            }
            data.Append("]");
            string TreeViewDate = data.ToString();
            return TreeViewDate;
        }

        public void TreeViewRecurive(Permission node, StringBuilder data)
        {

            data.Append($@"{{ 'id': '{node.Value}',  'text': '{node.Title}'");
            if (node.ChildTable.Count > 0)
            {
                data.Append(",'children':[");
                foreach (Permission child in node.ChildTable)
                {
                    TreeViewRecurive(child, data);
                    data.Append(",");
                }
                data.Append(']');
            }
            data.Append('}');
        }

        public string ShowCurrentPermissionTree(string userPermission)
        {
            string[] ArrayofPermission= userPermission.Split(',');

            StringBuilder ListofPermission= new StringBuilder("[");
            foreach (var item in ArrayofPermission)
            {
                ListofPermission.Append("\'"+item+ "\'"+",");
            }
            ListofPermission.Append("]");
            return ListofPermission.ToString();
        }

    }
    //public class UpdateMemberCustomFilterProvider : IFilterProvider
    //{

    //    private readonly IDependencyResolver _resolver;
    //    public UpdateMemberCustomFilterProvider(IDependencyResolver resolver)
    //    {
    //        _resolver = resolver;
    //    }

    //    public IEnumerable<System.Web.Mvc.Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
    //    {

    //        var filter = _resolver.GetService(typeof(UpdateMemberPremission)) as UpdateMemberPremission;

    //        //var filter = new UpdateMemberPremission(); // Create a new instance of your custom filter
    //        return new[] { new System.Web.Mvc.Filter(filter, FilterScope.Global, null) };
    //    }

    //}
}