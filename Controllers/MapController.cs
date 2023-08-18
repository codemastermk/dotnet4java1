﻿using Microsoft.AspNetCore.Mvc;
using night_life_sk.Dto.Event;
using night_life_sk.Dto.Place;
using night_life_sk.Services;

namespace night_life_sk.Controllers
{
    [Route("api/v1/map")]
    [ApiController]
    public class MapController : Controller
    {
        private readonly BaseMapService mapService;
        public MapController(BaseMapService mapService)
        {
            this.mapService = mapService;
        }
        [HttpGet("/coordinates")]
        [ProducesResponseType(200, Type = typeof(HashSet<PlaceCoordinates>))]
        public IActionResult GetAllPlaces() => Ok(mapService.GetAllPartyPlaces());

        [HttpGet("/place-on-click")]
        [ProducesResponseType(200, Type = typeof(PlaceAndEventDto))]
        public IActionResult GetPlaceAndEventOnClick (
            [FromQuery] double longitude,
            [FromQuery] double latitude,
            [FromQuery] DateTime date)
        {
            return Ok(mapService.GetPlaceAndEventOnClick(longitude, latitude, date));
        }

        [HttpGet("/events/{date}")]
        [ProducesResponseType(200, Type = typeof(HashSet<PartyEventDto>))]
        public ActionResult<HashSet<PartyEventDto>> GetAllEventsByDate(DateTime date)
        {
            return Ok(mapService.GetEventsByDate(date));
        }

        [HttpGet("/events/filtered")]
        [ProducesResponseType(200, Type = typeof(HashSet<PartyEventDto>))]
        public ActionResult<HashSet<PartyEventDto>> GetFilteredEvents(
            [FromQuery] int price,
            [FromQuery] string genre,
            [FromQuery] DateTime date)
        {
            return Ok(mapService.GetFilteredEvents(price, genre, date));
        }
    }
}
