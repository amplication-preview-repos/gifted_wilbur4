using DiscordBotIntegration.APIs.Common;
using DiscordBotIntegration.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotIntegration.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindManyArgs : FindManyInput<User, UserWhereInput> { }
