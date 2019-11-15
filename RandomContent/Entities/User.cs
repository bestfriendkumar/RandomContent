﻿namespace RandomContent.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Access { get; set; }
        public string Token { get; set; }
    }

    public enum Role
    {
        User,
        BetterUser
    }
}