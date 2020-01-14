using SorensenDice.Helper.Models;
using System.Collections.Generic;

namespace SorensenDice.Helper.Interfaces
{
    public interface IDataService
    {
        List<MedicalTerminology> Search(string searchTerm);
    }
}