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
var animations_1 = require("@angular/animations");
var competition_model_1 = require("../../core/models/competition.model");
var store_service_1 = require("../../core/store.service");
var router_1 = require("@angular/router");
var responsive_service_1 = require("../../core/responsive.service");
var CompetitionsPanelComponent = /** @class */ (function () {
    function CompetitionsPanelComponent(store, responsive, router) {
        this.store = store;
        this.responsive = responsive;
        this.router = router;
        this.displayed = true;
        // check responsive
        if (!this.responsive.isFull()) {
            this.displayed = this.router.url == '/';
        }
    }
    CompetitionsPanelComponent.prototype.ngOnInit = function () {
        // subscribe competitions$
        this.competitions$ = this.store.select('data/competitions');
    };
    CompetitionsPanelComponent.prototype.getCompetitionName = function (league) {
        return competition_model_1.Competition.getName(league);
    };
    CompetitionsPanelComponent.prototype.hide = function () {
        if (!this.responsive.isFull()) {
            this.displayed = false;
        }
    };
    CompetitionsPanelComponent = __decorate([
        core_1.Component({
            selector: 'home-competitions-panel',
            templateUrl: 'competitions-panel.component.html',
            styleUrls: ['competitions-panel.component.scss'],
            host: {
                '(document:mouseup)': 'hide()',
                '[@side]': 'displayed ? "active" : "inactive"',
            },
            animations: [
                animations_1.trigger('side', [
                    animations_1.state('inactive', animations_1.style({
                        transform: 'translateX(-100%)'
                    })),
                    animations_1.state('active', animations_1.style({
                        transform: 'translateX(0)'
                    })),
                    animations_1.transition('inactive => active', animations_1.animate('200ms ease-in')),
                    animations_1.transition('active => inactive', animations_1.animate('200ms ease-out'))
                ]),
                animations_1.trigger('top', [
                    animations_1.state('inactive', animations_1.style({
                        transform: 'translateY(-100%)'
                    })),
                    animations_1.state('active', animations_1.style({
                        transform: 'translateY(0)'
                    })),
                    animations_1.transition('inactive => active', animations_1.animate('200ms ease-in')),
                    animations_1.transition('active => inactive', animations_1.animate('200ms ease-out'))
                ]),
            ]
        }),
        __metadata("design:paramtypes", [store_service_1.StoreService,
            responsive_service_1.ResponsiveService, typeof (_a = typeof router_1.Router !== "undefined" && router_1.Router) === "function" && _a || Object])
    ], CompetitionsPanelComponent);
    return CompetitionsPanelComponent;
    var _a;
}());
exports.CompetitionsPanelComponent = CompetitionsPanelComponent;
//# sourceMappingURL=competitions-panel.component.js.map