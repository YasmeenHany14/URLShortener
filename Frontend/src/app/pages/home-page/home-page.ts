import {Component, DestroyRef, inject, OnDestroy, signal} from '@angular/core';
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
import {takeUntilDestroyed} from '@angular/core/rxjs-interop';
import {Card} from 'primeng/card';
import {ProgressSpinner} from 'primeng/progressspinner';
import {ToastModule} from 'primeng/toast';

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
    Message,
    ProgressSpinner,
    Card,
    ToastModule
  ],
  templateUrl: './home-page.html',
  styleUrl: './home-page.scss',
})
export class HomePage {
  loading = signal(false);
  error = signal<string | null>(null);
  private urlService = inject(UrlService);
  private messageService = inject(MessageService);
  private destroyRef = inject(DestroyRef);

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
    this.loading.set(true);
    this.generatedUrl.shortUrlCode = '';
    this.generateUrl(this.shortenUrlform.value.originalUrl!);
    return;
  }

  generateUrl(originalUrl: string) {
    this.shortenUrlDetails.originalUrl = originalUrl;
    const subscription = this.urlService.GenerateUrl(this.shortenUrlDetails)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
      next: (response) => {
        this.generatedUrl.shortUrlCode = `${environment.baseUrl}/${response.shortUrlCode}`;
        this.loading.set(false);
      },
      error: (error) => {
        this.loading.set(false);
        this.messageService.add({
          severity: 'error',
          summary: 'URL Shortening Failed',
          detail: error?.message || 'An error occurred. Please try again.',
          life: 4000,
          closable: true
        });
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
