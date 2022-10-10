using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Game
    {
        public int id;
        public String name;
        public Boolean isNew;
        public DateTime last_save;
        public List<Era> eras;
        public Game(){}

    }
}