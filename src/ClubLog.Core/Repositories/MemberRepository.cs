using DbThing;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ClubLog.Core.Repositories;

public class MemberRepository(IConfiguration config, ILogger logger) : DbRepository(config)
{
}