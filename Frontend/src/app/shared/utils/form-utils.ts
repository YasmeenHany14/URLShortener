import {FormGroup} from '@angular/forms';
import {ErrorMessages} from './error-messages';

export function getErrorMessages(form: FormGroup, controlName: string): string[] {
  const control = form.get(controlName);
  if (!control || !control.errors) return [];

  return Object.keys(control.errors).map(key => {
    const error = control.errors![key];
    switch (key) {
      case 'minlength':
      case 'maxlength':
        return ErrorMessages.messages[key].replace('0', error.requiredLength);
      default:
        return ErrorMessages.getErrorMessage(key);
    }
  });
}

export function isInvalid(form: FormGroup, controlName: string): boolean {
  const control = form.get(controlName);
  let ans = !!(control && control.invalid && (control.dirty || control.touched));
  return ans;
}
