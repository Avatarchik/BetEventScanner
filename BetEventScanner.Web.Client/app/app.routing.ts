import { Statement } from '@angular/compiler/src/output/output_ast';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { HomeComponent } from './home/home.component';
import { StatsComponent } from './stats/stats.component';
import { EventScanComponent } from './event-scan/event-scan.component';
import { ProfileComponent } from './profile/profile.component';
import { NotFoundComponent } from './not-found.component';

const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'stats',
        component: StatsComponent
    },
    {
        path: 'event-scan',
        component: EventScanComponent
    },
    {
        path: 'profile',
        component: ProfileComponent
    },
    {
        path: '**',
        component: NotFoundComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})

export class AppRouting { }