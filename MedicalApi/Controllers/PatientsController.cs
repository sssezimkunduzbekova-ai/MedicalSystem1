
using Microsoft.AspNetCore.Mvc;
using MedicalApi.Models;

namespace MedicalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private static List<Patient> _patients = new List<Patient>();

    [HttpGet]
    public List<Patient> GetAll()
    {
        return _patients;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Patient> GetById([FromRoute] int id)
    {
        Patient? patient = _patients.FirstOrDefault(p => p.Id == id);

        if (patient == null)
        {
            return NotFound();
        }

        return patient;
    }

    [HttpPost]
    public ActionResult<Patient> Create(Patient patient)
    {
        patient.Id = _patients.Count + 1;
        _patients.Add(patient);

        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Patient> Update([FromRoute] int id, Patient updatedPatient)
    {
        Patient? patient = _patients.FirstOrDefault(p => p.Id == id);

        if (patient == null)
        {
            return NotFound();
        }

        patient.Name = updatedPatient.Name;
        patient.Surname = updatedPatient.Surname;
        patient.BirthDate = updatedPatient.BirthDate;
        patient.PhoneNumber = updatedPatient.PhoneNumber;
        patient.Address = updatedPatient.Address;
        patient.BloodType = updatedPatient.BloodType;
        patient.HasChronicDisease = updatedPatient.HasChronicDisease;

        return Ok(patient);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        Patient? patient = _patients.FirstOrDefault(p => p.Id == id);

        if (patient == null)
        {
            return NotFound();
        }

        _patients.Remove(patient);

        return NoContent();
    }
}
