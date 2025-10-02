using System;

namespace UserManagement.DTO
{
    public static class Session
    {
        public static string CurrentConnectionString { get; set; }
        public static string CurrentRole { get; set; }
        public static string CurrentUsername { get; set; }
    }
}
