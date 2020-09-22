﻿using FieldFactory.DataAccess.DTO;
using System;

namespace FieldFactory.DataAccess.SQLite
{
    internal static class SQLiteExploreStringBuilder
    {
        internal static string AddExploreQuery(ExploreDTO dto)
        {
            return $"INSERT INTO Explore (idPlayer, idFollower, idLocation, idEvent, idStep, dateNextStep, stepsHistory) VALUES " +
                $"('{dto.IdPlayer}', '{dto.IdFollower}', '{dto.IdLocation}', '{dto.IdEvent}', '{dto.IdStep}', '{dto.DateNextStep}', '{dto.StepHistory}')";
        }
    }
}
