"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var http_service_1 = require('../shared/http.service');
var HomeComponent = (function () {
    function HomeComponent(httpService) {
        this.httpService = httpService;
    }
    HomeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.httpService.getCompetition()
            .subscribe(function (competition) { return _this.competitions = competition; });
        this.httpService.getFixtures()
            .subscribe(function (fixtures) { return _this.fixtures = fixtures; });
        this.httpService.getTable()
            .subscribe(function (tableStands) { return _this.tableStands = tableStands; });
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'app-home',
            template: "\n<div class=\"panel panel-primary\" *ngIf=\"competitions\">\n    <div class=\"panel-heading\">\n        <h3 class=\"panel-title\">Latest Updated</h3>\n    </div>\n    <div class=\"panel-body\">\n        <div class=\"row\" *ngFor=\"let competition of competitions; let i = index\">\n            <div class=\"col-md-12\">\n                <span class=\"list-group-item\">\n                    {{competition.caption}}\n                    <span class=\"badge\">{{competition.currentMatchday}}</span>\n                    <span class=\"label label-danger\">{{competition.lastUpdated}}</span>\n                </span>\n            </div>\n        </div>\n    </div>\n</div>\n\n<div class=\"panel panel-success\" *ngIf='tableStands'>\n    <div class=\"panel-heading\">\n        <h3 class=\"panel-title\">{{tableStands.leagueCaption}}</h3>\n    </div>\n    <div class=\"panel-body\">\n        <table class=\"table table-hover\">\n            <thead>\n                <tr>\n                    <th>Pos</th>\n                    <th>Team</th>\n                    <th>P</th>\n                    <th>W</th>\n                    <th>D</th>\n                    <th>L</th>\n                    <th>GF</th>\n                    <th>GA</th>\n                    <th>Pts</th>\n                </tr>\n            </thead>\n            <tbody>\n                <tr *ngFor='let team of tableStands | http; let i = index'>\n                    <td>{{team.value.position}}</td>\n                    <td><img class=\"team-logo\" src=\"{{team.value.crestURI}}\"><span class=\"team-name\">{{team.value.teamName}}</span></td>\n                    <td>{{team.value.playedGames}}</td>\n                    <td>{{team.value.wins}}</td>\n                    <td>{{team.value.draws}}</td>\n                    <td>{{team.value.losses}}</td>\n                    <td>{{team.value.goals}}</td>\n                    <td>{{team.value.goalsAgainst}}</td>\n                    <td>{{team.value.points}}</td>\n                </tr>\n            </tbody>\n        </table>\n    </div>\n</div>\n",
            styles: ["\n.team-logo {\n    display: inline-block;\n    width: 33px;\n    height: 33px;    \n}\n\n.team-name {\n    padding-left: 30px;\n}\n\n.table>tbody>tr>td {\n    line-height: 30px;\n}\n\n.label-danger {\n    margin-left: 40px;\n}\n"]
        }), 
        __metadata('design:paramtypes', [http_service_1.HttpService])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map