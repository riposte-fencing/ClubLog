using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;
using ClubLog.Core.Models.Enums;
using DbThing;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ClubLog.Core.Repositories;

public class BaseRepository(IConfiguration configuration, ILogger logger) : DbRepository(configuration) 
{
}