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
var http_1 = require('@angular/http');
require('rxjs/add/operator/map');
var HttpService = (function () {
    function HttpService(http) {
        this.http = http;
        this.competitionUrl = 'http://api.football-data.org/v1/competitions/?season=2016';
        this.fixturesUrl = 'http://api.football-data.org/v1/competitions/426/fixtures';
        this.leagueTable = 'http://api.football-data.org/v1/competitions/426/leagueTable';
        this.headers = new http_1.Headers({ 'X-Auth-Token': '07343b6896a74d57920afd88bed1a68f' });
        this.options = new http_1.RequestOptions({ headers: this.headers });
    }
    HttpService.prototype.getCompetition = function () {
        return this.http.get(this.competitionUrl, this.options)
            .map(function (response) { return response.json(); });
    };
    HttpService.prototype.getFixtures = function () {
        return this.http.get(this.fixturesUrl, this.options)
            .map(function (response) { return response.json(); });
    };
    HttpService.prototype.getTable = function () {
        return this.http.get(this.leagueTable, this.options)
            .map(function (response) { return response.json()["standing"]; });
    };
    HttpService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], HttpService);
    return HttpService;
}());
exports.HttpService = HttpService;
//# sourceMappingURL=http.service.js.map