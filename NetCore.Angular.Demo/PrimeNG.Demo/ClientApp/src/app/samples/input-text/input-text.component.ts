import { Component } from '@angular/core';

@Component({
  selector: 'input-text-component',
  templateUrl: './input-text.component.html'
})
export class InputTextComponent {
  disabled: boolean = true;

  value1: string = null!;

  value2: string = null!;

  value3: string = null!;

  value4: string = null!;

  value5: string = 'Disabled';
}
