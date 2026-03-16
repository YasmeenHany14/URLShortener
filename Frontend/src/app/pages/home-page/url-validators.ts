import {AbstractControl, ValidationErrors} from '@angular/forms';

export function urlFormatValid(control: AbstractControl): ValidationErrors | null {
  if(!control.value || control.value === "") {
    return null;
  }

  try {
    new URL(control.value);
    return null;
  }
  catch (e) {
    return { invalidUrl: true };
  }
}
