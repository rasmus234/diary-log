pipeline{
    agent any
    stages{
        stage("Build API") {
            when {
                anyOf {
                    changeset "DiaryLog/**"
                }
            }
            steps{
                dir("DiaryLog") {
                    sh "dotnet build --configuration Release"
                    sh "sudo docker-compose build api"
                }
            }
        }
        stage("Build frontend") {
            when {
                changeset "diary-log-angular/**"
            }
            steps {
                dir("diary-log-angular") {
                    sh "sudo docker-compose build web"
                }
            }
        }
        stage("Unit Tests") {
            steps {
                echo "Here we'll run unit tests later"
            }
        }
        stage("Clean containers") {
            steps {
                script {
                    try {
                        sh "sudo docker-compose down"
                    }
                    finally { }
                }
            }
        }
        stage("Deploy") {
            steps {
                sh "sudo docker-compose up -d"
            }
        }
    }
}