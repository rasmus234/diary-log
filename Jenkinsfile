pipeline {
    agent any
    
    stages {
        stage('Build') {
            parallel {
                stage('Build API') {
                    steps {
                        sh 'dotnet build DiaryLog/DiaryLog.sln'
                    }
                }

                stage('Build Front End') {
                    steps {
                        dir('diary-log-angular') {
                            sh 'npm install'
                            sh 'npx ng build diary-log-angular'
                        }
                    }
                }
            }
        }
        
        /*
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
        */
    }
}