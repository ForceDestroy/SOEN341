//Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

//Components
import { SettingsComponent } from './settings.component';
import { SharedModule } from '../shared/shared.module';

const profileRoutes: Routes = [
  {path: '', component: SettingsComponent},
];

@NgModule({
  declarations: [SettingsComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(profileRoutes),
  ]
})
export class SettingsModule { }
