﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SlimFormaturas.Domain.Entities {
    public class Contract : Entity{
        public string ContractId { get; set; }

        public string Code { get; set; }
        public int Semester { get; set; }
        public int year { get; set; }
        //RESPOSAVEL PELO CONTRATO
        //QUEM FECHOU O CONTRATO
        //RESPONSÁVEL CERIMONIAL
        public string Note { get; set; }
        public DateTime DateRegister { get; protected set; }



        public string CollegeId { get; set; }

        public College College { get; set; }
        public IList<Course> Course { get; set; }
    }
}
