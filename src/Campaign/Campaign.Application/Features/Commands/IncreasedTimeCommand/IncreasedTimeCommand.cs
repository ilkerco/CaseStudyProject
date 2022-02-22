using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Application.Features.Commands.IncreasedTimeCommand
{
    public class IncreasedTimeCommand:IRequest
    {
        public int IncreasedCount { get; set; }

        public IncreasedTimeCommand(int ıncreasedCount)
        {
            IncreasedCount = ıncreasedCount;
        }
    }
}
