//Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

//Components
import { HomeComponent } from './home.component';

const homeRoutes: Routes = [
  {path: '', component: HomeComponent},
];



@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(homeRoutes),
  ]
})
export class HomeModule { }
