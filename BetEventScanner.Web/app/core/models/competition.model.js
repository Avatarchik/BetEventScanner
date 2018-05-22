"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Competition = /** @class */ (function () {
    function Competition() {
        this.id = null;
        this.caption = null;
        this.league = null;
        this.year = null;
        this.currentMatchday = null;
        this.numberOfMatchdays = null;
        this.numberOfTeams = null;
        this.numberOfGames = null;
        this.lastUpdated = null;
        this.fixtures = [];
        this.table = null;
    }
    Competition.getName = function (league) {
        var names = {
            'PL': 'Premier League',
            'ELC': 'Championship',
            'EL1': 'League One',
            'BL1': '1. Bundesliga',
            'BL2': '2. Bundesliga',
            'DED': 'Eredivisie',
            'FL1': 'Ligue 1',
            'FL2': 'Ligue 2',
            'PD': 'Primera Division',
            'SD': 'Liga Adelante',
            'SA': 'Serie A',
            'PPL': 'Primeira Liga',
        };
        return names[league] || league;
    };
    Competition.isAvailable = function (competition) {
        return ['PL', 'ELC', 'EL1', 'BL1', 'BL2', 'DED', 'FL1', 'FL2', 'PD', 'SD', 'SA', 'PPL'].indexOf(competition.league) !== -1;
    };
    return Competition;
}());
exports.Competition = Competition;
//# sourceMappingURL=competition.model.js.map