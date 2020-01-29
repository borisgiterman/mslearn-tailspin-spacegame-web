using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TailSpin.SpaceGame.Web;
using TailSpin.SpaceGame.Web.Models;
using NUnit.Framework;

using Tailspin.SpaceGame.Web.Controllers;

namespace Tests
{
    public class GameControllerTests
    {
        private GameController _gameController;
        private IDocumentDBRepository<Score> _scoreRepository;
        private IDocumentDBRepository<Profile> profileRespository;
        [SetUp]
        public void Setup()
        {
            using (Stream scoresData = typeof(IDocumentDBRepository<Score>)
                .Assembly
                .GetManifestResourceStream("Tailspin.SpaceGame.Web.SampleData.scores.json"))
            {
                _scoreRepository = new LocalDocumentDBRepository<Score>(scoresData);
            }
            using (Stream profilesData = typeof(IDocumentDBRepository<Score>)
                            .Assembly
                            .GetManifestResourceStream("Tailspin.SpaceGame.Web.SampleData.profiles.json"))
            {
                profileRespository = new LocalDocumentDBRepository<Profile>(profilesData);
            }
            _gameController =new GameController(_scoreRepository, profileRespository);
        }

        [TestCase(ExpectedResult =25)]
        public int CountGamePlayers()
        {
            List<PlayerScore> playerScores = (List<PlayerScore>)Convert.ChangeType
                (_gameController.GetPlayers().Result, typeof(List<PlayerScore>));
            return playerScores.Count;
        }

    }
}