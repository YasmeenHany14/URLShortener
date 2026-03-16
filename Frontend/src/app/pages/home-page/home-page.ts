import {Component, inject, signal} from '@angular/core';
import {UrlService} from '../../core/services/url-service';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {InputText} from 'primeng/inputtext';
import {FloatLabel} from 'primeng/floatlabel';
import {InputIcon} from 'primeng/inputicon';
import {IconField} from 'primeng/iconfield';
import {ButtonModule} from 'primeng/button';
import {GenerateUrlRequestModel, GenerateUrlResponseModel} from '../../core/models/url-model';
import {urlFormatValid} from './url-validators';
import {getErrorMessages, isInvalid} from '../../shared/utils/form-utils';
import {Message} from 'primeng/message';
import {MessageService} from 'primeng/api';
import {ErrorMessages} from '../../shared/utils/error-messages';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-home-page',
  imports: [
    ReactiveFormsModule,
    InputText,
    FormsModule,
    FloatLabel,
    InputIcon,
    IconField,
    ButtonModule,
    Message
  ],
  templateUrl: './home-page.html',
  styleUrl: './home-page.scss',
})
export class HomePage {
  loading = signal(true);
  error = signal<string | null>(null);
  private urlService = inject(UrlService);
  private messageService = inject(MessageService);

  getErrorMessages = getErrorMessages;
  isInvalid = isInvalid;

  shortenUrlDetails: GenerateUrlRequestModel = {
    originalUrl: '',
  }
  generatedUrl: GenerateUrlResponseModel = {
    shortUrlCode: '',
  }

  shortenUrlform = new FormGroup({
    originalUrl: new FormControl<string>(this.shortenUrlDetails.originalUrl, {
      validators: [Validators.required, urlFormatValid],
      updateOn: 'blur'
    })
  })

  onSubmit() {
    if (this.shortenUrlform.invalid) {
      this.messageService.add({
        severity: 'error',
        summary: 'Form Error',
        detail: 'Please correct the errors in the form.',
        life: 3000,
        closable: true
      });
      return;
    }
    this.shortenUrlDetails.originalUrl = this.shortenUrlform.value.originalUrl!;


  }

  generateUrl(originalUrl: string) {
    const subscription = this.urlService.GenerateUrl(this.shortenUrlDetails).subscribe({
      next: (response) => {
        this.generatedUrl.shortUrlCode = `${environment.baseUrl}/${response.shortUrlCode}`;
      },
      error: (error) => {

      }
    })
  }

  getErrorMessage(controlName: string): string | null {
    const control = this.shortenUrlform.get(controlName);
    if (control && control.errors) {
      const errorKey = Object.keys(control.errors)[0];
      return ErrorMessages.getErrorMessage(errorKey);
    }
    return null;
  }
}
