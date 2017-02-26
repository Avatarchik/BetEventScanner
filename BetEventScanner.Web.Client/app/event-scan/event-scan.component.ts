import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, FormArray, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-event-scan',
    template: `
<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title">Form Select</h3>
    </div>
    <div class="panel-body">
        <form novalidate [formGroup]="selectForm" (ngSubmit)='onSubmit()'>
            <div formArrayName="results">
                <a class="btn btn-success" (click)="add()">
                    Add
                </a>

                <div class="list-group" *ngFor="let result of results.controls; let i=index" [formGroupName]="i">
                    <span class="list-group-item">
                        Choose the result of match №{{i+1}}:
                           <label class="radio-inline">
                              <input type="radio" class="form-control" value="winning" formControlName="result"> W
                           </label>

                           <label class="radio-inline">
                              <input type="radio" class="form-control" value="drawing" formControlName="result"> D
                           </label>

                           <label class="radio-inline">
                              <input type="radio" class="form-control" value="losing" formControlName="result"> L
                           </label>
                    </span>
                </div>
            </div>
            <div class="panel-footer">
                <button class="btn btn-primary" type="submit">Send Data</button>
            </div>
        </form>
    </div>
</div>
<div class="panel panel-warning">
    <div class="panel-heading">
        <h3 class="panel-title">Form Request</h3>
    </div>
    <div class="panel-body">
        <pre>{{ selectForm.value | json }}</pre>
    </div>
</div> 
`,
    styles: [`
.form-control {
  height: auto;
  width: auto;
}

.list-group {
  margin-bottom: 0;
}

.radio-inline {
  margin-left: 30px;
}
`]
    //encapsulation: ViewEncapsulation.None
})
export class EventScanComponent {
    selectForm: FormGroup;
    results: FormArray;
    events = ['win', 'draw', 'loss'];

    constructor(private _fb: FormBuilder) {
        this.selectForm = this._fb.group({
            results: this.buildArray()
        });
    }

    buildArray(): FormArray {
        this.results = this._fb.array([
            this.buildGroup()
        ]);
        return this.results;
    }

    buildGroup(): FormGroup {
        return this._fb.group({
            result: ''
        });
    }

    add() {
        this.results.push(this.buildGroup());
    }

    onSubmit() {
        console.log(this.selectForm.value);
    }
}
