using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicDiary.Models;
using ElectronicDiary.Repositories;

namespace ElectronicDiary.Services;

public class GradeService
{
    private readonly GradeRepository _repository = new();

    public Task<List<GradeModel>> GetGradesAsync()
        => _repository.GetGradesAsync();

    public Task AddGradeAsync(GradeModel grade)
        => _repository.AddGradeAsync(grade);

    public Task UpdateGradeAsync(GradeModel grade)
        => _repository.UpdateGradeAsync(grade);

    public Task DeleteGradeAsync(GradeModel grade)
        => _repository.DeleteGradeAsync(grade);
}