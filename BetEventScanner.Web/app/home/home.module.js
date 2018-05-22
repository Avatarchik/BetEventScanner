"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var home_routing_1 = require("./home.routing");
var home_page_component_1 = require("./home-page/home-page.component");
var main_layout_component_1 = require("./main-layout/main-layout.component");
var competitions_panel_component_1 = require("./competitions-panel/competitions-panel.component");
var competition_page_component_1 = require("./competition-page/competition-page.component");
var fetch_competitions_resolver_1 = require("./fetch-competitions.resolver");
var fetch_fixtures_resolver_1 = require("./fetch-fixtures.resolver");
var fetch_table_resolver_1 = require("./fetch-table.resolver");
var fixture_line_component_1 = require("./fixture-line/fixture-line.component");
var league_table_component_1 = require("./league-table/league-table.component");
var match_time_pipe_1 = require("./match-time.pipe");
var match_date_pipe_1 = require("./match-date.pipe");
var HomeModule = /** @class */ (function () {
    function HomeModule() {
    }
    HomeModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                home_routing_1.HomeRoutingModule,
            ],
            exports: [],
            declarations: [
                main_layout_component_1.MainLayoutComponent,
                competitions_panel_component_1.CompetitionsPanelComponent,
                home_page_component_1.HomePageComponent,
                competition_page_component_1.CompetitionPageComponent,
                fixture_line_component_1.FixtureLineComponent,
                league_table_component_1.LeagueTableComponent,
                match_date_pipe_1.MatchDatePipe,
                match_time_pipe_1.MatchTimePipe,
            ],
            providers: [
                fetch_competitions_resolver_1.FetchCompetitionsResolver,
                fetch_fixtures_resolver_1.FetchFixturesResolver,
                fetch_table_resolver_1.FetchTableResolver,
            ],
        })
    ], HomeModule);
    return HomeModule;
}());
exports.HomeModule = HomeModule;
//# sourceMappingURL=home.module.js.map