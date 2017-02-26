import { Component, OnInit } from '@angular/core';
import { HttpService } from '../shared/http.service';
import { HttpPipe } from '../shared/http.pipe';

@Component({
    selector: 'app-home',
    template: `
<div class="panel panel-primary" *ngIf="competitions">
    <div class="panel-heading">
        <h3 class="panel-title">Latest Updated</h3>
    </div>
    <div class="panel-body">
        <div class="row" *ngFor="let competition of competitions; let i = index">
            <div class="col-md-12">
                <span class="list-group-item">
                    {{competition.caption}}
                    <span class="badge">{{competition.currentMatchday}}</span>
                    <span class="label label-danger">{{competition.lastUpdated}}</span>
                </span>
            </div>
        </div>
    </div>
</div>

<div class="panel panel-success" *ngIf='tableStands'>
    <div class="panel-heading">
        <h3 class="panel-title">{{tableStands.leagueCaption}}</h3>
    </div>
    <div class="panel-body">
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>Pos</th>
                    <th>Team</th>
                    <th>P</th>
                    <th>W</th>
                    <th>D</th>
                    <th>L</th>
                    <th>GF</th>
                    <th>GA</th>
                    <th>Pts</th>
                </tr>
            </thead>
            <tbody>
                <tr *ngFor='let team of tableStands | http; let i = index'>
                    <td>{{team.value.position}}</td>
                    <td><img class="team-logo" src="{{team.value.crestURI}}"><span class="team-name">{{team.value.teamName}}</span></td>
                    <td>{{team.value.playedGames}}</td>
                    <td>{{team.value.wins}}</td>
                    <td>{{team.value.draws}}</td>
                    <td>{{team.value.losses}}</td>
                    <td>{{team.value.goals}}</td>
                    <td>{{team.value.goalsAgainst}}</td>
                    <td>{{team.value.points}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
`,
    styles: [`
.team-logo {
    display: inline-block;
    width: 33px;
    height: 33px;    
}

.team-name {
    padding-left: 30px;
}

.table>tbody>tr>td {
    line-height: 30px;
}

.label-danger {
    margin-left: 40px;
}
`]
})
export class HomeComponent implements OnInit {
    competitions: any[];
    fixtures: any[];
    tableStands: any[];

    constructor(private httpService: HttpService) { }

    ngOnInit() {
        this.httpService.getCompetition()
            .subscribe(competition => this.competitions = competition);

        this.httpService.getFixtures()
            .subscribe(fixtures => this.fixtures = fixtures);

        this.httpService.getTable()
            .subscribe(tableStands => this.tableStands = tableStands);
    }

}
