import { Component } from '@angular/core';

@Component({
    selector: 'app-stats',
    template: `
<h3>
    {{title}}
</h3>
`
})
export class StatsComponent {
    title = 'stats component works';
}
