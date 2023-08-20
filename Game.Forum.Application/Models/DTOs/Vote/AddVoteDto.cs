using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Forum.Application.Models.DTOs.Vote
{
    public class AddVoteDto
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool? Voted { get; set; }
    }
}
