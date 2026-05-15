using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicDiary.Data;
using ElectronicDiary.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDiary.Repositories;

public class GradeRepository
{
    private readonly DiaryDbContext _db = new();

    public async Task<List<GradeModel>> GetGradesAsync()
        => await _db.Grades.ToListAsync();

    public async Task AddGradeAsync(GradeModel grade)
    {
        _db.Grades.Add(grade);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateGradeAsync(GradeModel grade)
    {
        _db.Grades.Update(grade);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteGradeAsync(GradeModel grade)
    {
        _db.Grades.Remove(grade);
        await _db.SaveChangesAsync();
    }
}