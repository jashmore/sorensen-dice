using SorensenDice.Helper.Models;
using System.Collections.Generic;

namespace SorensenDice.Helper.Interfaces
{
    public interface IDataHelper
    {
        List<MedicalProcedure> GetAll();
    }
}