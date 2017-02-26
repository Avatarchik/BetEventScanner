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
var github_service_1 = require('../shared/github.service');
var ProfileComponent = (function () {
    function ProfileComponent(githubService) {
        var _this = this;
        this.githubService = githubService;
        this.githubService.getUser().subscribe(function (user) {
            _this.user = user;
        });
        this.githubService.getRepos().subscribe(function (repos) {
            _this.repos = repos;
        });
    }
    ProfileComponent = __decorate([
        core_1.Component({
            selector: 'app-profile',
            template: "\n<div *ngIf=\"user\">\n    <div class=\"panel panel-primary\">\n        <div class=\"panel-heading\">\n            <h2 class=\"panel-title\">{{user.name}}</h2>\n        </div>\n        <div class=\"panel-body\">\n            <div class=\"row\">\n                <div class=\"col-md-3 col-lg-3\">\n                    <img class=\"img-thumbnail img-responsive\" src=\"{{user.avatar_url}}\" alt=\"avatar\">\n                    <a class=\"btn btn-primary btn-block\" href=\"{{user.html_url}}\" target=\"_blank\">View Profile on GitHub</a>\n                </div>\n                <div class=\"col-md-9\">\n                    <div class=\"user-stats\">\n                        <span class=\"label label-default\">{{user.public_repos}} Public Repos</span>\n                        <span class=\"label label-primary\">{{user.public_gists}} Public Gists</span>\n                        <span class=\"label label-success\">{{user.followers}} Followers</span>\n                        <span class=\"label label-info\">{{user.following}} Following</span>\n                    </div>\n                    <br>\n                    <ul class=\"list-group\">\n                        <li class=\"list-group-item\"><strong>Username: </strong>{{user.login}}</li>\n                        <li class=\"list-group-item\"><strong>Location: </strong>{{user.location}}</li>\n                        <li class=\"list-group-item\"><strong>Email: </strong>{{user.email}}</li>\n                        <li class=\"list-group-item\"><strong>Blog: </strong>{{user.blog}}</li>\n                        <li class=\"list-group-item\"><strong>Member Since: </strong>{{user.created_at}}</li>\n                    </ul>\n                </div>\n            </div>\n        </div>\n    </div>\n    <div class=\"panel panel-primary\">\n        <div class=\"panel-heading\">\n            <h3 class=\"panel-title\">User Repos</h3>\n        </div>\n        <div class=\"panel-body\">\n            <div *ngFor='let repo of repos'>\n                <div class=\"row\">\n                    <div class=\"col-md-9\">\n                        <h4><a href=\"{{repo.html_url}}\" target=\"_blank\">{{repo.name}}</a></h4>\n                        <p>{{repo.description}}</p>\n                    </div>\n                    <div class=\"col-md-3\">\n                        <span class=\"label label-default\">{{repo.watchers}} Watchers</span>\n                        <span class=\"label label-primary\">{{repo.forks}} Forks</span>\n                    </div>\n                </div>\n                <hr>\n            </div>\n        </div>\n    </div>\n</div>\n",
            styles: ["\nimg {\n    width: 100%;\n}\n"]
        }), 
        __metadata('design:paramtypes', [github_service_1.GithubService])
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;
//# sourceMappingURL=profile.component.js.map