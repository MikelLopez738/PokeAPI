﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Application.Model
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attribute
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class EffectEntry
    {
        public string effect { get; set; }
        public Language language { get; set; }
        public string short_effect { get; set; }
    }

    public class FlavorTextEntry
    {
        public Language language { get; set; }
        public string text { get; set; }
        public VersionGroup version_group { get; set; }
    }

    public class GameIndexItem
    {
        public int game_index { get; set; }
        public Generation generation { get; set; }
    }

    public class Generation
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Name
    {
        public Language language { get; set; }
        public string name { get; set; }
    }

    public class RootItem
    {
        public List<Attribute> attributes { get; set; }
        public object baby_trigger_for { get; set; }
        public Category category { get; set; }
        public int cost { get; set; }
        public List<EffectEntry> effect_entries { get; set; }
        public List<FlavorTextEntry> flavor_text_entries { get; set; }
        public object fling_effect { get; set; }
        public object fling_power { get; set; }
        public List<GameIndex> game_indices { get; set; }
        public List<object> held_by_pokemon { get; set; }
        public int id { get; set; }
        public List<object> machines { get; set; }
        public string name { get; set; }
        public List<Name> names { get; set; }
        public SpritesItem sprites { get; set; }
        public string sprite { get; set; }
    }

    public class SpritesItem
    {
        public string @default { get; set; }
    }

    public class VersionGroupItem
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
