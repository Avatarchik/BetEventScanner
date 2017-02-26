import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class HttpService {

    private competitionUrl = 'http://api.football-data.org/v1/competitions/?season=2016';
    private fixturesUrl = 'http://api.football-data.org/v1/competitions/426/fixtures';
    private leagueTable = 'http://api.football-data.org/v1/competitions/426/leagueTable';

    private headers = new Headers({ 'X-Auth-Token': '07343b6896a74d57920afd88bed1a68f' });
    private options = new RequestOptions({ headers: this.headers });

    constructor(private http: Http) { }

    getCompetition() {
        return this.http.get(this.competitionUrl, this.options)
            .map(response => response.json());
    }

    getFixtures() {
        return this.http.get(this.fixturesUrl, this.options)
            .map(response => response.json());
    }

    getTable() {
        return this.http.get(this.leagueTable, this.options)
            .map(response => response.json()["standing"]);
    }

}