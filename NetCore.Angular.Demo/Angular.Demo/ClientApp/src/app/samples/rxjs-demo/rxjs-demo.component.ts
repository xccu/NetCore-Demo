import { Component, Inject } from '@angular/core';
import { Observable, delay, filter, map, mergeMap, of } from 'rxjs';

@Component({
  selector: 'app-rxjs-demo-component',
  templateUrl: './rxjs-demo.component.html',
  styleUrls: ['./rxjs-demo.component.css']
})

export class RxjsDemoComponent {

  constructor() {
  }

  public values: string[] | undefined;


  public onMap() {
    var nums = of(1, 2, 3);  /** source observable of integers. */
    nums.pipe(
      map(num => num * num) // `map` configured with a function that squares each value.
    ).subscribe(
      (value: any) => { console.log(value); this.values = value; }
    );
  }

  public onMergeMap() {
    var strs = of("Calcifer", "Alchemist", "X-Men", "Link");
    strs.pipe(
        mergeMap(arr => this.dummyApi(arr)) // gets 4 Observable as API response and merges them
      ).subscribe( // we subscribe to one mapped and merged Observable
        (data:any) => console.log(data)
      )
  }

  public dummyApi = (item: any) => {
    return of(`API response for data: ${item}`).pipe( //
      map(item => "[GET]"+item)
    );
  }

  public onFilter() {
    var squareOdd = of(1, 2, 3, 4, 5);
    squareOdd.pipe(
        filter(n => n % 2 === 1),
        map(n => n * n)
    );

    // Subscribe to get values
    squareOdd.subscribe(x => console.log(x));
  }
}
