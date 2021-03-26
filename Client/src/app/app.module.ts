//Angular Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http';

//Components
import { AppComponent } from './app.component';

//Modules
import { AuthenticationModule } from './authentication/authentication.module';
import { HomeModule } from './home/home.module';
import { ProfileModule } from './profile/profile.module';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PostModule } from './post/post.module';
import { SettingsModule } from './settings/settings.module';

const appRoutes: Routes = [
  {path: 'auth', loadChildren: () => AuthenticationModule},
  {path: 'home', loadChildren: () => HomeModule},
  {path: 'user', loadChildren: () => ProfileModule},
  {path: 'post', loadChildren: () => PostModule},
  {path: 'settings', loadChildren: () => SettingsModule},
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
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
    ),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
