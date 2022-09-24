﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class PokemonType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public List<Pokemon> Pokemon { get; set; }
    }
}