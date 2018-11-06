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
var rxjs_1 = require("rxjs");
var router_1 = require("@angular/router");
var store_service_1 = require("../../core/store.service");
var CompetitionPageComponent = /** @class */ (function () {
    function CompetitionPageComponent(route, store, zone) {
        this.route = route;
        this.store = store;
        this.zone = zone;
        this.competition$ = new rxjs_1.BehaviorSubject(null);
        this.fixtureHover$ = new rxjs_1.BehaviorSubject([]);
        this.fixtures$ = new rxjs_1.BehaviorSubject([]);
        this.league$ = new rxjs_1.BehaviorSubject(null);
        this.subs = [];
        this.table$ = new rxjs_1.BehaviorSubject([]);
        this.tour$ = new rxjs_1.BehaviorSubject(null);
        // responsive
        this.responsiveTab = '-fixtures';
        // animations
        this.linesChange$ = new rxjs_1.BehaviorSubject('completed');
        this.nextTour$ = new rxjs_1.BehaviorSubject(null);
    }
    CompetitionPageComponent.prototype.ngOnDestroy = function () {
        this.subs.forEach(function (sub) {
            sub.unsubscribe();
        });
    };
    CompetitionPageComponent.prototype.ngOnInit = function () {
        var _this = this;
        console.log('INIT!!!');
        // subscribe league$
        this.route.params.map(function (params) { return params['league']; }).subscribe(this.league$);
        // subscribe competition$
        var competitionChange$ = this.league$.map(function (league) { return _this.store.state.data.competitions.find(function (x) { return x.league == league; }); });
        competitionChange$.subscribe(this.competition$);
        // subscribe tour$
        competitionChange$.map(function (x) { return x.currentMatchday; }).subscribe(this.tour$);
        competitionChange$.map(function (x) { return x.currentMatchday; }).subscribe(this.nextTour$);
        // subscribe $fixtures
        // react on league, tour or store competitions changes
        rxjs_1.Observable.combineLatest(this.league$, this.tour$, this.store.select('data/competitions'), function (league, tour, competitions) {
            return { league: league, tour: tour, competitions: competitions };
        }).map(function (combined) { return combined.competitions.find(function (x) { return x.league == _this.league$.value; }).fixtures.filter(function (x) { return x.matchday == _this.tour$.value; }); })
            .subscribe(this.fixtures$);
        // check fixtures and fetch more if needed
        var dataPending = false;
        this.subs.push(this.fixtures$.subscribe(function (fixtures) {
            if (!dataPending && fixtures.length === 0) {
                dataPending = true;
                _this.store.data.fetchFixtures(_this.competition$.value.id, _this.tour$.value).subscribe(function (length) {
                    if (length == 0) {
                        throw new Error('Empty fetch!');
                    }
                    dataPending = false;
                });
            }
        }));
        // subscribe table$
        this.competition$.map(function (competition) { return competition.table.standing; }).subscribe(this.table$);
        // animation completing
        var animationCompeted$ = this.nextTour$.debounceTime(200);
        animationCompeted$.mapTo('completed').subscribe(this.linesChange$);
        animationCompeted$.subscribe(this.tour$);
    };
    CompetitionPageComponent.prototype.isFixtureHighlighted = function (fixture) {
        var hovered = this.fixtureHover$.value;
        return hovered.indexOf(fixture.homeTeamName) !== -1 || hovered.indexOf(fixture.awayTeamName) !== -1;
    };
    CompetitionPageComponent.prototype.nextTour = function () {
        if (this.nextTour$.value < this.competition$.value.numberOfMatchdays) {
            this.linesChange$.next('right');
            this.nextTour$.next(this.nextTour$.value + 1);
        }
    };
    CompetitionPageComponent.prototype.prevTour = function () {
        if (this.nextTour$.value > 1) {
            this.linesChange$.next('left');
            this.nextTour$.next(this.nextTour$.value - 1);
        }
    };
    CompetitionPageComponent = __decorate([
        core_1.Component({
            selector: 'home-competition-page',
            templateUrl: 'competition-page.component.html',
            styleUrls: ['competition-page.component.scss'],
            animations: [
                animations_1.trigger('change', [
                    animations_1.state('completed', animations_1.style({
                        transform: 'translateX(0)'
                    })),
                    animations_1.state('left', animations_1.style({
                        transform: 'translateX(+100%)'
                    })),
                    animations_1.state('right', animations_1.style({
                        transform: 'translateX(-100%)'
                    })),
                    animations_1.transition('right => completed', [
                        animations_1.animate(300, animations_1.keyframes([
                            animations_1.style({ opacity: 0, transform: 'translateX(+100%)', offset: 0 }),
                            animations_1.style({ opacity: 1, transform: 'translateX(-20px)', offset: 0.7 }),
                            animations_1.style({ opacity: 1, transform: 'translateX(0)', offset: 1.0 })
                        ]))
                    ]),
                    animations_1.transition('left => completed', [
                        animations_1.animate(300, animations_1.keyframes([
                            animations_1.style({ opacity: 0, transform: 'translateX(-100%)', offset: 0 }),
                            animations_1.style({ opacity: 1, transform: 'translateX(+20px)', offset: 0.7 }),
                            animations_1.style({ opacity: 1, transform: 'translateX(0)', offset: 1.0 })
                        ]))
                    ]),
                    animations_1.transition('completed => *', animations_1.animate('200ms ease-out')),
                ]),
            ]
        }),
        __metadata("design:paramtypes", [typeof (_a = typeof router_1.ActivatedRoute !== "undefined" && router_1.ActivatedRoute) === "function" && _a || Object, store_service_1.StoreService, typeof (_b = typeof core_1.NgZone !== "undefined" && core_1.NgZone) === "function" && _b || Object])
    ], CompetitionPageComponent);
    return CompetitionPageComponent;
    var _a, _b;
}());
exports.CompetitionPageComponent = CompetitionPageComponent;
//# sourceMappingURL=competition-page.component.js.map