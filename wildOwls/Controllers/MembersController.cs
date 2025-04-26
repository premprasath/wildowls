using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WildOwls.Domain.Contract;
using WildOwls.Model;


namespace WildOwls.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMembersDomain _membersDomain;
        public MembersController(IMembersDomain membersDomain)
        {
            _membersDomain = membersDomain;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //var groupDetails = await _membersDomain.GetAllGroupInfoAsync();
                //return View(groupDetails);
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetAllGroupInfoAsync()
        {
            try
            {
                var groupInfo = await _membersDomain.GetAllGroupInfoAsync();
                return new JsonResult(groupInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetFamilyInfobyIdAsync(string groupId)
        {
            try
            {
                var memberInfo = await _membersDomain.GetFamilyInfobyIdAsync(groupId);
                return new JsonResult(memberInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(MemberInfo memberInfo)
        {
            try
            {
                //if (ModelState.IsValid == true)
                //{
                    var insertedDoctorId = await _membersDomain.InsertMemberAsync(memberInfo);
                //}

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(memberInfo);
            }
        }
    }
}
