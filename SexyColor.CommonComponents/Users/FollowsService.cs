using System;
using System.Collections.Generic;
using SexyColor.BusinessComponents;

namespace SexyColor.CommonComponents
{
    public class FollowsService : IFollowsService
    {

        public IFollowsRepository followsRepository { get; set; }

        public IEnumerable<long> GetFocusIds(long userId)
        {
            return followsRepository.GetFocusIds(userId);
        }

        public IEnumerable<long> GetFollowedIds(long userId)
        {
            return followsRepository.GetFollowedIds(userId);
        }

    }
}
