//Angular Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms'

//Components
import { AppComponent } from './app.component';

//Modules
import { AuthenticationModule } from './authentication/authentication.module';
import { HomeModule } from './home/home.module';
import { ProfileModule } from './profile/profile.module';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PostModule } from './post/post.module';

const appRoutes: Routes = [
  {path: 'auth', loadChildren: () => AuthenticationModule},
  {path: 'home', loadChildren: () => HomeModule},
  {path: 'user', loadChildren: () => ProfileModule},
  {path: 'post', loadChildren: () => PostModule},
  {path: '', redirectTo: 'home', pathMatch: 'full'},
];

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    SharedModule,
    RouterModule.forRoot(
      appRoutes,
    ),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
