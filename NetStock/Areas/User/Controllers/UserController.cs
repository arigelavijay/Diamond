using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetStock.Areas.User.Controllers
{
    public class LayoutMenuRights
    {
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public List<SecurablesRights> securablesLst { get; set; }
    }


    public class SecurablesRights
    {
        // Constructor 
        public SecurablesRights() { }

        // Public Members 

        public string SecurableItem { get; set; }

        public string GroupID { get; set; }

        public string Description { get; set; }

        public string ActionType { get; set; }

        public string Link { get; set; }

        public string Icon { get; set; }

        public bool hasRight { get; set; }

        public Int32 Sequence { get; set; }

        public Int32 ParentSequence { get; set; }

        public List<SecurablesRights> ActionMenus { get; set; }

    }

    public class RoleRightsMenu
    {
        // Constructor 
        public RoleRightsMenu() { }

        // Public Members 

        public string RoleCode { get; set; }

        public string SecurableItem { get; set; }

        public bool hasRight { get; set; }
    }

    [Authorize]
    [RouteArea("User")]
    [ActionFilters.SessionFilter]
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        #region Users
        [Route("UserList")]
        [HttpGet]
        public ActionResult UserList()
        {
            var lstUsers = new NetStock.BusinessFactory.UsersBO().GetList();
            return View("UserList", lstUsers);
        }

        [Route("EditUser")]
        [HttpGet]
        public ActionResult EditUser(string userID)
        {
            var user = new NetStock.Contract.Users();

            if (userID == "NEW")
            {
                userID = "";
                user = new Contract.Users();
            }


            if (userID != null && userID.Length > 0)
                user = new NetStock.BusinessFactory.UsersBO().GetUsers(new Contract.Users { UserID = userID });


            user.RoleCodeList = new NetStock.BusinessFactory.RolesBO().GetList().Select(x => new SelectListItem
            {
                Value = x.RoleCode,
                Text = x.RoleDescription
            }).ToList();

            return View("UserProfile", user);
        }


        [Route("SaveUser")]
        [HttpPost]
        public ActionResult SaveUser(NetStock.Contract.Users user)
        {
            try
            {
                user.LogInStatus = true;
                user.CreatedBy = Session["DEFAULTUSER"].ToString();
                user.ModifiedBy = Session["DEFAULTUSER"].ToString();
                var result = new NetStock.BusinessFactory.UsersBO().SaveUsers(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            var lstUsers = new NetStock.BusinessFactory.UsersBO().GetList();
            return View("UserList", lstUsers);
        }


        [Route("RoleRights")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult RoleRights(string Role = "")
        {
            List<LayoutMenuRights> lstMenu = new List<LayoutMenuRights>();
            if (!string.IsNullOrWhiteSpace(Role))
            {
                var lstUsers = new NetStock.BusinessFactory.UsersBO().GetList();
                var roleRights = new NetStock.BusinessFactory.RoleRightsBO()
                                    .GetList(Role);

                var securablesAll = (List<NetStock.Contract.Securables>)System.Web.HttpContext.Current.Application["AppSecurables"];

                var securables = securablesAll.Join(roleRights,
                                    sec => sec.SecurableItem,
                                    rig => rig.SecurableItem,
                                    (sec, rig) => new { a = sec, b = rig })
                                .Select(x => new NetStock.Contract.Securables()
                                {
                                    SecurableItem = x.a.SecurableItem,
                                    GroupID = x.a.GroupID,
                                    Description = x.a.Description,
                                    ActionType = x.a.ActionType,
                                    Link = x.a.Link,
                                    Icon = x.a.Icon,
                                    Sequence = x.a.Sequence,
                                    ParentSequence = x.a.ParentSequence
                                })
                                .ToList<NetStock.Contract.Securables>();


                var menuItems = securablesAll.Where(x => x.ActionType == "TopMenu")
                                    .Select(x => new { securableItem = x.SecurableItem, Icon = x.Icon, GroupId = x.GroupID }).Distinct().ToList();


                for (var i = 0; i < menuItems.Count; i++)
                {
                    LayoutMenuRights item = new LayoutMenuRights();
                    item.MenuName = menuItems[i].securableItem;
                    item.Icon = menuItems[i].Icon;
                    item.securablesLst = securablesAll.Where(x => x.GroupID == menuItems[i].securableItem && (x.ActionType == "Menu"))
                                                   .Select(x => new SecurablesRights
                                                   {
                                                       SecurableItem = x.SecurableItem,
                                                       GroupID = x.GroupID,
                                                       Description = x.Description,
                                                       ActionType = x.ActionType,
                                                       Link = x.Link,
                                                       Icon = x.Icon,
                                                       hasRight = (securables.Where(j => j.SecurableItem == x.SecurableItem).Count() > 0),
                                                       Sequence = x.Sequence,
                                                       ParentSequence = x.ParentSequence,
                                                       ActionMenus = securablesAll.Where(y => y.GroupID == menuItems[i].securableItem && (y.ActionType == "Action") && y.ParentSequence == x.Sequence)
                                                                                   .Select(y => new SecurablesRights
                                                                                   {
                                                                                       SecurableItem = y.SecurableItem,
                                                                                       GroupID = y.GroupID,
                                                                                       Description = y.Description,
                                                                                       ActionType = y.ActionType,
                                                                                       Link = y.Link,
                                                                                       Icon = y.Icon,
                                                                                       hasRight = (securables.Where(jk => jk.SecurableItem == y.SecurableItem).Count() > 0),
                                                                                       Sequence = y.Sequence,
                                                                                       ParentSequence = y.ParentSequence
                                                                                   }).ToList<SecurablesRights>()
                                                   }).OrderBy(x => x.ParentSequence).ToList<SecurablesRights>();

                    if (item.securablesLst.Count > 0)
                    {
                        lstMenu.Add(item);
                    }
                }

                ViewBag.RoleCode = Role;
            }

            return View("RoleRights", lstMenu);
        }

        [HttpPost]
        [Route("SaveRights")]
        public ActionResult SaveRights(List<RoleRightsMenu> right)
        {
            try
            {
                var lstRoleRights = new List<NetStock.Contract.RoleRights>();

                right.Where(r => r.hasRight == true)
                    .ToList()
                    .ForEach(r => lstRoleRights.Add(new Contract.RoleRights { RoleCode = r.RoleCode, SecurableItem = r.SecurableItem }));

                var result = new NetStock.BusinessFactory.RoleRightsBO().SaveRoleRights(lstRoleRights);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }

            return RedirectToAction("RoleRights");
        }

        #endregion

    }
}