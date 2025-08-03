using Microsoft.AspNetCore.Mvc;
using AidCare.Business;
using AidCare.Entities;

namespace AidCare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodGlucoseController : ControllerBase
    {
        private readonly IBloodGlucoseService _bloodGlucoseService;

        public BloodGlucoseController(IBloodGlucoseService bloodGlucoseService)
        {
            _bloodGlucoseService = bloodGlucoseService;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BloodGlucose>>> GetAllRecords()
        {
            try
            {
                var records = await _bloodGlucoseService.GetAllAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kan şekeri kayıtları getirilirken hata oluştu.", error = ex.Message });
            }
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<BloodGlucose>> GetRecord(int id)
        {
            try
            {
                var record = await _bloodGlucoseService.GetByIdAsync(id);
                if (record == null)
                {
                    return NotFound(new { message = "Kan şekeri kaydı bulunamadı." });
                }
                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kan şekeri kaydı getirilirken hata oluştu.", error = ex.Message });
            }
        }

        // GET
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BloodGlucose>>> GetRecordsByUserId(int userId)
        {
            try
            {
                var records = await _bloodGlucoseService.GetByUserIdAsync(userId);
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcının kan şekeri kayıtları getirilirken hata oluştu.", error = ex.Message });
            }
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<BloodGlucose>> CreateRecord(BloodGlucose bloodGlucose)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdRecord = await _bloodGlucoseService.CreateAsync(bloodGlucose);
                return CreatedAtAction(nameof(GetRecord), new { id = createdRecord.Id }, createdRecord);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kan şekeri kaydı oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<BloodGlucose>> UpdateRecord(int id, BloodGlucose bloodGlucose)
        {
            try
            {
                if (id != bloodGlucose.Id)
                {
                    return BadRequest(new { message = "ID uyumsuzluğu." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedRecord = await _bloodGlucoseService.UpdateAsync(bloodGlucose);
                return Ok(updatedRecord);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kan şekeri kaydı güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecord(int id)
        {
            try
            {
                await _bloodGlucoseService.DeleteAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kan şekeri kaydı silinirken hata oluştu.", error = ex.Message });
            }
        }
    }
}