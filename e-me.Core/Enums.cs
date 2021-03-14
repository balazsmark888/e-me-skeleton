using System.ComponentModel.DataAnnotations;

namespace e_me.Core
{
    public static class Enums
    {
        public enum SecurityType
        {
            /// <summary>
            /// The app administrator.
            /// </summary>
            [Display(Name = "SecurityType_Administrator", ResourceType = typeof(Resources))]
            AppAdministrator = 0,

            /// <summary>
            /// The user.
            /// </summary>
            [Display(Name = "SecurityType_RegularUser", ResourceType = typeof(Resources))]
            RegularUser = 1,

            /// <summary>
            /// The viewer.
            /// </summary>
            [Display(Name = "SecurityType_Viewer", ResourceType = typeof(Resources))]
            Viewer = 2
        }

        public enum SessionStatus
        {
            /// <summary>
            /// Session status active
            /// </summary>
            [Display(Name = "SessionStatus_Active", ResourceType = typeof(Resources))]
            Inactive = 0,

            /// <summary>
            /// Session status inactive
            /// </summary>
            [Display(Name = "SessionStatus_Inactive", ResourceType = typeof(Resources))]
            Active = 1
        }
    }
}
