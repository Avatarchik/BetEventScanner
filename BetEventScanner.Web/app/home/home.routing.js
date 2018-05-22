"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var home_page_component_1 = require("./home-page/home-page.component");
var main_layout_component_1 = require("./main-layout/main-layout.component");
var competition_page_component_1 = require("./competition-page/competition-page.component");
var fetch_competitions_resolver_1 = require("./fetch-competitions.resolver");
var fetch_fixtures_resolver_1 = require("./fetch-fixtures.resolver");
var fetch_table_resolver_1 = require("./fetch-table.resolver");
var routes = [
    {
        path: '',
        component: main_layout_component_1.MainLayoutComponent,
        resolve: {
            fetchData: fetch_competitions_resolver_1.FetchCompetitionsResolver,
        },
        children: [
            {
                path: '',
                component: home_page_component_1.HomePageComponent,
            },
            {
                path: 'competition/:league',
                component: competition_page_component_1.CompetitionPageComponent,
                resolve: {
                    fixtures: fetch_fixtures_resolver_1.FetchFixturesResolver,
                    table: fetch_table_resolver_1.FetchTableResolver,
                },
            },
        ],
    },
];
var HomeRoutingModule = /** @class */ (function () {
    function HomeRoutingModule() {
    }
    HomeRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(routes)],
            exports: [router_1.RouterModule],
        })
    ], HomeRoutingModule);
    return HomeRoutingModule;
}());
exports.HomeRoutingModule = HomeRoutingModule;
//# sourceMappingURL=home.routing.js.map