﻿using System;
using System.Text;
using System.Text.Json.Serialization;

namespace App.Core.Domain.Entities
{
    public class GitRepoDetails
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [JsonPropertyName("homepage")]
        public Uri Homepage { get; set; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime LastPushUtc { get; set; }

        public DateTime LastPush => LastPushUtc.ToLocalTime();

        /// <summary>
        /// How each list should be formated
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"\"{Name}\", \"{Description}\", \"{GitHubHomeUrl}\", \"{Homepage}\", {Watchers}, {LastPushUtc}, {LastPush}";
        }
    }
}