using CasoUno.Principal.Front.Models.Contexto;
using CasoUno.Principal.Front.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace CasoUno.Principal.Front.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        #region Propiedades de Seguridad
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();  }
            set { _userManager = value; }
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();  }
            private set { _signInManager = value; }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        
        }

        #endregion


        public AccountController()
        {
             
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) 
        {
            _signInManager = signInManager;
            UserManager = userManager;

        }


        #region Apartado de Login


        [HttpGet]
        [AllowAnonymous]
        
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVM loginVM, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var result = await SignInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password,
                loginVM.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("","Credenciales Incorrectas.");
                    return View(loginVM);
            }




        }
        #endregion

        #region Apartado para Registro

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) 
            {
                return View(registerVM);
            }

            var user = new ApplicationUser
            {
                UserName = registerVM.Email, Email = registerVM.Email

            };
            var result = await UserManager.CreateAsync(
                user,registerVM.Password
                );

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user,false,false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("",error);
            }
            return View(registerVM);
        }


        #endregion


        #region Logout y RedirectLocal
        private ActionResult RedirectToLocal(string returnUrl) {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index","Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login","Account");
        }
        #endregion


        #region Liberar Recursos

        protected override void Dispose(bool disposing) {
            if (disposing)
            {
                _userManager?.Dispose();
                _signInManager?.Dispose();
            }
          base.Dispose(disposing);
        }
        #endregion



    }
}