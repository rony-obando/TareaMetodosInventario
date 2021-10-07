using Domain.Entities.Empleados;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Procesos
{
    public class SalaryCalculator
    {
        private decimal InssRate => 0.07M;
        private TarifaIR[] tarifaIRs = new TarifaIR[]
        {
            new TarifaIR(){Desde = 0.01M, Hasta= 100000,ImpuestoBase =0,ProcentajeAplicable=0,SobreExceso =0},
            new TarifaIR(){Desde = 100000.01M, Hasta= 200000,ImpuestoBase =0,ProcentajeAplicable=0.15M,SobreExceso =100000},
            new TarifaIR(){Desde = 200000.01M, Hasta= 350000,ImpuestoBase =15000,ProcentajeAplicable=0.20M,SobreExceso =200000},
            new TarifaIR(){Desde = 350000.01M, Hasta= 500000,ImpuestoBase =45000,ProcentajeAplicable=0.25M,SobreExceso =350000},
            new TarifaIR(){Desde = 500000.01M, Hasta= 100000000000,ImpuestoBase =82500,ProcentajeAplicable=0.30M,SobreExceso =500000},
        };
        private decimal CalculateAnnualSalary(decimal salary)
        {
            return salary * 12;
        }
        private decimal CalculateInss(decimal salary)
        {
            return salary * InssRate;
        }
        private decimal CalculateIr(decimal Asalary)
        {
            decimal ir = decimal.MinValue;
            //ir = 1.0M;
            foreach(TarifaIR tarifaIR in tarifaIRs)
            {
                if ((Asalary-tarifaIR.Desde)*(tarifaIR.Hasta-Asalary)>=0)
                {
                    ir = ((((Asalary - tarifaIR.SobreExceso) * tarifaIR.ProcentajeAplicable) + tarifaIR.ImpuestoBase) / 12);
                    break;
                }
            }
            return ir;
        }
        private decimal CalculateSalaryperHours(decimal Salary)
        {
            return Salary/160;
        }
        private decimal CalculateExtraHoursSalary(float horasExtras, decimal SalaryPerHour)
        {
            return (decimal)horasExtras * SalaryPerHour;
        }
        public decimal CalculateSalary(Empleado empleado)
        {
            decimal salary = decimal.MinValue;
            decimal inss = CalculateInss(empleado.Salario);
            decimal annualsalary = decimal.MinValue, ir = decimal.MinValue, partialsalary = decimal.MinValue;

            if(empleado is Docente){
                annualsalary= CalculateAnnualSalary(empleado.Salario - inss);
               // ir = CalculateIr(annualsalary);
                partialsalary = empleado.Salario;
            }
            else if(empleado is Administrativo)
            {
                partialsalary= CalculateAnnualSalary(empleado.Salario + CalculateExtraHoursSalary(((Administrativo)empleado).HorasExtras, CalculateSalaryperHours(empleado.Salario)));
                annualsalary = CalculateAnnualSalary(partialsalary-inss);
            }
            ir = CalculateIr(annualsalary);
            salary = partialsalary - inss - ir;
            return salary;
        }
    }
}
