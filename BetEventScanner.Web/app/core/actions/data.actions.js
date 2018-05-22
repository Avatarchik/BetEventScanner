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
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var rxjs_1 = require("rxjs");
var store_service_1 = require("../store.service");
var competition_model_1 = require("../models/competition.model");
var DataActions = /** @class */ (function () {
    function DataActions() {
        var _this = this;
        this.init = function (store) {
            _this.store = store;
            _this.store.dispatch(function (state) {
                state.data = {
                    competitions: [],
                };
                return state;
            });
        };
        // #################
        // #### ACTIONS ####
        // #################
        this.fetchCompetitions = function () {
            return new rxjs_1.Observable(function (observer) {
                _this.store
                    .get('https://api.football-data.org/v1/competitions')
                    .subscribe(function (data) {
                    if (data) {
                        _this.$$setCompetitions(data.filter(competition_model_1.Competition.isAvailable));
                        observer.next(_this.store.state);
                        observer.complete();
                        return;
                    }
                    observer.error('Invalid response!');
                });
            });
        };
        this.fetchFixtures = function (competitionId, matchday) {
            return new rxjs_1.Observable(function (observer) {
                _this.store
                    .get("https://api.football-data.org/v1/competitions/" + competitionId + "/fixtures?matchday=" + matchday)
                    .subscribe(function (data) {
                    if (data && data.fixtures) {
                        _this.$$setFixtures(competitionId, data.fixtures.map(function (fixture) {
                            fixture.id = fixture._links.self.href.split('/').pop();
                            return fixture;
                        }));
                        observer.next(data.fixtures.length);
                        observer.complete();
                        return;
                    }
                    observer.error('Invalid response!');
                });
            });
        };
        this.fetchTable = function (competitionId) {
            return new rxjs_1.Observable(function (observer) {
                _this.store
                    .get("https://api.football-data.org/v1/competitions/" + competitionId + "/leagueTable")
                    .subscribe(function (data) {
                    if (data) {
                        _this.$$setTable(competitionId, data);
                        observer.next(_this.store.state);
                        observer.complete();
                        return;
                    }
                    observer.error('Invalid response!');
                });
            });
        };
        // #####################
        // #### DISPATCHERS ####
        // #####################
        this.$$setCompetitions = function (competitions) {
            _this.store.dispatch(function (state) {
                state.data.competitions = store_service_1.StoreService.__copyCollection(competitions.map(function (competition) { return Object.assign(new competition_model_1.Competition(), competition); }));
                return state;
            });
        };
        this.$$setFixtures = function (competitionId, fixtures) {
            _this.store.dispatch(function (state) {
                var currentCompetition = state.data.competitions.find(function (competition) { return competition.id == competitionId; });
                var newFixtures = store_service_1.StoreService.__mergeCollections(currentCompetition.fixtures, fixtures);
                var newCompetition = store_service_1.StoreService.__setObjectField(currentCompetition, 'fixtures', newFixtures);
                state.data.competitions = store_service_1.StoreService.__setCollectionItem(state.data.competitions, newCompetition);
                return state;
            });
        };
        this.$$setTable = function (competitionId, table) {
            _this.store.dispatch(function (state) {
                var competition = store_service_1.StoreService.__setObjectField(state.data.competitions.find(function (competition) { return competition.id == competitionId; }), 'table', table);
                state.data.competitions = store_service_1.StoreService.__setCollectionItem(state.data.competitions, competition);
                return state;
            });
        };
    }
    DataActions = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], DataActions);
    return DataActions;
}());
exports.DataActions = DataActions;
//# sourceMappingURL=data.actions.js.map