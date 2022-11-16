﻿using System.Collections.Generic;

namespace DB_LAB_TESTS
{
    public class Row {
        public List<string> Values { get; set; } = new List<string>();

        public string this[int i] {
            get => Values[i];
            set => Values[i] = value;
        }
    }
}
